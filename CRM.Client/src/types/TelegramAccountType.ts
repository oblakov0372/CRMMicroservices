export enum Status {
  Scamer = "Scamer",
  Reseller = "Reseller",
}

export interface TelegramAccountLite {
  id: string;
  telegramId: number;
  telegramUsername: string | null;
  status: Status | null;
}
