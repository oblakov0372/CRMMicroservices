export enum DealStatus {
  "None",
  "InProgress",
  "Pending",
  "LostDeal",
  "WonDeal",
  "Cart",
  "Scamer",
}

export type DealType = {
  id: string;
  status: DealStatus;
  createdById: string;
  createdByUserName: string;
  createdDate: string;
  telegramUserId: string;
};
