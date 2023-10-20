import { DealStatus } from "../types/DealType";
import { Status } from "../types/TelegramAccountLiteType";

export const formatDate = (dateString: string) => {
  const date = new Date(dateString);

  const options: Intl.DateTimeFormatOptions = {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
    second: "2-digit",
  };

  return date.toLocaleDateString(undefined, options);
};

export const getStatusString = (status: Status | null): string => {
  switch (status) {
    case Status.None:
      return "None";
    case Status.Scamer:
      return "Scamer";
    case Status.Reseller:
      return "Reseller";
    case Status.Inwork:
      return "Inwork";
    default:
      return "Unknown"; // Обработка неизвестных значений, если нужно
  }
};

export const getDealStatusString = (status: DealStatus | null): string => {
  switch (status) {
    case DealStatus.None:
      return "None";
    case DealStatus.InProgress:
      return "In Progress";
    case DealStatus.Pending:
      return "Pending";
    case DealStatus.LostDeal:
      return "Lost Deal";
    case DealStatus.WonDeal:
      return "Won Deal";
    case DealStatus.Cart:
      return "Cart";
    case DealStatus.Scamer:
      return "Scamer";
    default:
      return "Unknown"; // Обработка неизвестных значений, если нужно
  }
};
