import pymongo
import telethon
from telethon.sync import TelegramClient, events
from datetime import datetime
import re
import json
import uuid



with open('config.json', 'r') as json_file:
    config = json.load(json_file)

api_id = config["api_id"]
api_hash = config["api_hash"]

mongodb_uri_telegramMessage = config["mongodb_uri_telegramMessage"]
mongodb_uri_telegramAccount = config["mongodb_uri_telegramAccount"]

mongo_client_telegramMessage = pymongo.MongoClient(mongodb_uri_telegramMessage)
mongo_client_telegramAccount = pymongo.MongoClient(mongodb_uri_telegramAccount)

db_telegramMessages = mongo_client_telegramMessage["TelegramMessage"]
db_telegramAccounts = mongo_client_telegramAccount["TelegramUser"]

def should_save_message(message_text):
    if re.search(r'\?{5,}', message_text):
        return False
    return True

def user_exists(user_id):
    return db_telegramAccounts.telegramUsers.count_documents({"TelegramId": user_id}) > 0

def insert_user(user_id, username):
    if not user_exists(user_id):
        user_data = {
            "_id": str(uuid.uuid4()),
            "TelegramId": user_id,
            "TelegramUsername": username,
            "Status":0
        }
        db_telegramAccounts.telegramUsers.insert_one(user_data)

def insert_message(telegram_group_id, telegram_group_username, sender_id, sender_username, message, date, link_for_message, message_type):
    message_data = {
        "_id": str(uuid.uuid4()),
        "TelegramGroupId": telegram_group_id,
        "TelegramGroupUsername": telegram_group_username,
        "SenderId": sender_id,
        "SenderUsername": sender_username,
        "Message": message,
        "Date": date,
        "LinkForMessage": link_for_message,
        "Type": message_type,
    }
    db_telegramMessages.telegramMessages.insert_one(message_data)

with TelegramClient("session", api_id, api_hash) as client:
    group_ids = ["doubletop_otc", "tvrn_otc", "MarketICOBOG", "mediasocialmarket"]

    @client.on(events.NewMessage(chats=group_ids))
    async def handler(event):
        user = await event.get_sender()
        telegram_group_id = event.peer_id.channel_id
        telegram_group_username = (await event.get_chat()).username
        message = event.message.text
        date = datetime.now()
        link_for_message = f"https://t.me/{telegram_group_username}/{event.id}"


        if isinstance(user, telethon.tl.types.User):
            sender_id = user.id
            sender_username = user.username
        elif isinstance(user, telethon.tl.types.Chat) or isinstance(user, telethon.tl.types.Channel):
            sender_id = telegram_group_id  # Use the channel ID as the sender ID
            sender_username = "Channel"

        # Проверяем, содержит ли сообщение ключевые слова "wts" или "wtb"
        if "wts" in message.lower():
            message_type = "wts"
        elif "wtb" in message.lower():
            message_type = "wtb"
        else:
            message_type = None

        if should_save_message(message) and sender_username not in ("tvrn_help_bot", "shieldy_bot"):
            insert_user(sender_id, sender_username)
            insert_message(telegram_group_id, telegram_group_username, sender_id, sender_username, message, date, link_for_message, message_type)

    print("Listening for new messages and users...")
    client.run_until_disconnected()

mongo_client_telegramMessage.close()
mongo_client_telegramAccount.close()


