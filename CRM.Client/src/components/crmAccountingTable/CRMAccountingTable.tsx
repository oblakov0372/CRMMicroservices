import React from "react";
import { Link } from "react-router-dom";
import { TelegramAccountLite } from "../../types/TelegramAccountType";

type CrmTelegramAccountingTableProperty = {
  telegramAccounting: TelegramAccountLite[];
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
        {telegramAccounting.map((telegramAccount: TelegramAccountLite) => (
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
              {telegramAccount.telegramUsername !== null
                ? telegramAccount.telegramUsername
                : "None"}
            </td>
            <td className="font-extrabold px-3 py-2">
              {telegramAccount.status !== null
                ? telegramAccount.status
                : "None"}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default CrmAccountingTable;
