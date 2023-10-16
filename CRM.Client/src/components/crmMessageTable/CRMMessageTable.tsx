import { Link } from "react-router-dom";
import { TelegramMessageType } from "../../types/TelegramMessageType";
import { formatDate } from "../../utils/Utils";

type CRMMessageTableProperty = {
  telegramMessages: TelegramMessageType[];
  lyteVersion: boolean;
};

const CRMMessageTable: React.FC<CRMMessageTableProperty> = ({
  telegramMessages,
  lyteVersion,
}) => {
  return (
    <table className="text-center w-full bg-black text-white">
      <thead className="border-b-2">
        <tr>
          <th className="px-3 py-2" style={{ maxWidth: "80px" }}>
            Group Username
          </th>
          <th className="px-3 py-2" style={{ maxWidth: "50px" }}>
            Type
          </th>
          {!lyteVersion && (
            <th className="px-3 py-2" style={{ maxWidth: "50px" }}>
              Sender
            </th>
          )}
          <th className="px-3 py-2" style={{ maxWidth: "250px" }}>
            Message
          </th>
          <th className="px-3 py-2">Date</th>
        </tr>
      </thead>
      <tbody>
        {telegramMessages.map((telegramMessage: TelegramMessageType) => (
          <tr key={telegramMessage.id} className="border-b border-gray-400">
            <td
              className="font-extrabold px-3 py-2"
              style={{ maxWidth: "50px" }}
            >
              {telegramMessage.telegramGroupUsername}
            </td>
            <td
              className="font-extrabold px-3 py-2"
              style={{ maxWidth: "50px" }}
            >
              {telegramMessage.type?.toUpperCase()}
            </td>
            {!lyteVersion && (
              <td
                className="px-3 py-2 cursor-pointer"
                style={{ maxWidth: "50px" }}
              >
                <Link to={`/crm/accounting/${telegramMessage.senderId}`}>
                  {telegramMessage.senderUsername && (
                    <span className="font-bold text-blue-500 ">
                      {telegramMessage.senderUsername}
                    </span>
                  )}
                  <span className="block font-medium text-blue-500 hover:text-blue-700 ">
                    {telegramMessage.senderId}
                  </span>
                </Link>
              </td>
            )}
            <td className="px-3 py-2" style={{ maxWidth: "250px" }}>
              {telegramMessage.message}
            </td>
            <td style={{ maxWidth: "50px" }} className="px-3 py-2">
              {formatDate(telegramMessage.date)}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default CRMMessageTable;
