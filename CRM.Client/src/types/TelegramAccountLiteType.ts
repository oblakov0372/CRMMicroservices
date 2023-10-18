export enum Status {
  "None",
  "Scamer",
  "Reseller",
  "Inwork",
}

export type TelegramAccountLiteType = {
  id: string;
  telegramId: number;
  userName: string | null;
  status: Status | null;
};
