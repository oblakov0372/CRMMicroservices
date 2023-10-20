import { Status } from "./TelegramAccountLiteType";

export type TelegramAccountBaseType = {
  id: number;
  telegramId: number;
  userName: string | null;
  status: Status | null;
  firstActivity: string;
  lastActivity: string;
  countMessagesWtb: number;
  countMessagesWts: number;
  countAllMessages: number;
  linkToUserTelegram: string | null;
  linkToFirstMessage: string;
};
