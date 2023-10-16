import { Status } from "./TelegramAccountType";

export type TelegramAccountBaseType = {
  id: number;
  userName?: string | null;
  status?: Status | null;
  firstActivity: string;
  lastActivity: string;
  countMessagesWtb: number;
  countMessagesWts: number;
  countAllMessages: number;
  linkToUserTelegram?: string | null;
  linkToFirstMessage?: string | null;
};
