export type TelegramMessageType = {
  id: number;
  telegramGroupId: number;
  telegramGroupUsername: string | null;
  senderId: number;
  senderUsername: string | null;
  message: string | null;
  date: string;
  linkForMessage: string | null;
  type: string;
};
