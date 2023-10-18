import React from "react";
import { Link } from "react-router-dom";
import {
  Status,
  TelegramAccountLiteType,
} from "../../types/TelegramAccountLiteType";
import { getStatusString } from "../../utils/Utils";

type CrmTelegramAccountingTableProperty = {
  telegramAccounting: TelegramAccountLiteType[];
};

const CrmAccountingTable: React.FC<CrmTelegramAccountingTableProperty> = ({
  telegramAccounting,
}) => {
  return (
    <table className="text-center w-full bg-black text-white">
      <thead className="border-b-2">
        <tr>
          <th className="px-3 py-2">ID</th>
          <th className="px-3 py-2">Username</th>
          <th className="px-3 py-2">Status</th>
        </tr>
      </thead>
      <tbody>
        {telegramAccounting.map((telegramAccount: TelegramAccountLiteType) => (
          <tr key={telegramAccount.id} className="border-b border-gray-400">
            <td className="font-extrabold px-3 py-2 cursor-pointer ">
              <Link
                to={`${telegramAccount.telegramId}`}
                className="text-blue-500 hover:text-blue-700"
              >
                {telegramAccount.telegramId}
              </Link>
            </td>
            <td className="font-semibold px-3 py-2 ">
              {telegramAccount.userName ? telegramAccount.userName : "None"}
            </td>
            <td className="font-extrabold px-3 py-2">
              {getStatusString(telegramAccount.status)}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default CrmAccountingTable;
