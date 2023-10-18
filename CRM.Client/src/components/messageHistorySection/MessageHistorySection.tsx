import React from "react";
import LoadingSpinner from "../loadingSpinner/LoadingSpinner";
import { TelegramMessageType } from "../../types/TelegramMessageType";
import CRMMessageTable from "../crmMessageTable/CRMMessageTable";
import Pagination from "../pagination/Pagination";

type SearchAndTableProps = {
  searchQuery: string;
  setSearchQuery: (query: string) => void;
  messageType: string;
  handleMessageTypeChange: (type: string) => void;
  isLoadingMessages: boolean;
  telegramMessages: TelegramMessageType[];
  countPages: number;
  currentPage: number;
  setCurrentPage: (page: number) => void;
  pageSize: number;
  handlePageSize: (size: number) => void;
};

const MessageHistorySection: React.FC<SearchAndTableProps> = ({
  searchQuery,
  setSearchQuery,
  messageType,
  handleMessageTypeChange,
  isLoadingMessages,
  telegramMessages,
  countPages,
  currentPage,
  setCurrentPage,
  pageSize,
  handlePageSize,
}) => {
  return (
    <div>
      <div className="flex justify-between items-center bg-black px-7">
        <input
          className="bg-gray-600 px-3 py-1 rounded-sm"
          type="text"
          placeholder="Search..."
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
        />
        <div className="bg-black block text-center py-4">
          <label className="mr-2">ALL/WTS/WTB</label>
          <select
            className="bg-gray-600  px-4 py-1 rounded-sm"
            value={messageType}
            onChange={(e) => handleMessageTypeChange(e.target.value)}
          >
            <option value="all">All</option>
            <option value="wts">WTS</option>
            <option value="wtb">WTB</option>
          </select>
        </div>
      </div>
      {isLoadingMessages ? (
        <div className="">
          <LoadingSpinner />
        </div>
      ) : (
        <>
          <CRMMessageTable
            telegramMessages={telegramMessages}
            lyteVersion={true}
          />
          <div className="flex justify-between items-center">
            {countPages > 1 && (
              <Pagination
                currentPage={currentPage - 1}
                countPages={countPages}
                onChangePage={setCurrentPage}
              />
            )}
            <select
              className="bg-gray-600  px-4 py-1 rounded-sm "
              value={pageSize}
              onChange={(e) => handlePageSize(parseInt(e.target.value))}
            >
              <option value="5">5</option>
              <option value="10">10</option>
            </select>
          </div>
        </>
      )}
    </div>
  );
};

export default MessageHistorySection;
