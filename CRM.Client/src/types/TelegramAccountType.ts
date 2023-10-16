export enum Status {
  Scamer = "Scamer",
  Reseller = "Reseller",
}

export type TelegramAccountLiteType = {
  id: string;
  telegramId: number;
  telegramUsername: string | null;
  status: Status | null;
};
