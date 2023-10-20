import React from "react";
import { DealType } from "../../types/DealType";
import { toFormData } from "axios";
import { formatDate, getDealStatusString } from "../../utils/Utils";
import { Link } from "react-router-dom";
type PropsType = {
  deals: DealType[];
};

const CRMDealsTable: React.FC<PropsType> = ({ deals }) => {
  return (
    <table className="text-center w-full bg-black text-white">
      <thead className="border-b-2">
        <tr>
          <th className="px-3 py-2">Deal Number</th>
          <th className="px-3 py-2">Telegram Id</th>
          <th className="px-3 py-2">Status</th>
          <th className="px-3 py-2">Manager</th>
          <th className="px-3 py-2">Date</th>
        </tr>
      </thead>
      <tbody>
        {deals.map((deal, index) => (
          <tr key={deal.id} className="border-b border-gray-400">
            <td className="font-extrabold px-3 py-2">{index + 1}</td>
            <td className="font-extrabold px-3 py-2">
              <Link to={`/crm/accounting/${deal.telegramUserId}`}>
                {deal.telegramUserId && (
                  <span className="font-bold text-blue-500 hover:text-blue-700">
                    {deal.telegramUserId}
                  </span>
                )}
              </Link>
            </td>
            <td className="font-extrabold px-3 py-2">
              {getDealStatusString(deal.status)}
            </td>
            <td className="font-extrabold px-3 py-2">
              {deal.createdByUserName}
            </td>
            <td className="font-extrabold px-3 py-2">
              {formatDate(deal.createdDate)}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default CRMDealsTable;
