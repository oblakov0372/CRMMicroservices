import { useEffect, useState } from "react";
import { TelegramMessageType } from "../../types/TelegramMessageType";
import { QueryParamsType } from "../../types/QueryParamsType";
import { anonymRequest, authenticatedRequest } from "../../utils/Request";
import LoadingSpinner from "../../components/loadingSpinner/LoadingSpinner";
import Pagination from "../../components/pagination/Pagination";
import CRMMessageTable from "../../components/crmMessageTable/CRMMessageTable";
import { toErrorMessage } from "../../utils/ErrorHandler";

const CRMMessagePage = () => {
  const [telegramMessages, setTelegramMessages] = useState<
    TelegramMessageType[]
  >([]);
  const [countPages, setCountPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [isLoadingMessages, setIsLoadingMessages] = useState<Boolean>(true);
  const [isLoadingError, setIsLoadingError] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [messageType, setMessageType] = useState<string>("all");
  const [searchQuery, setSearchQuery] = useState<string>("");
  const [pageSize, setPageSize] = useState<number>(25);
  const [searchTimeout, setSearchTimeout] = useState<NodeJS.Timeout | null>(
    null
  );

  const fetchData = async () => {
    setIsLoadingError(false);
    try {
      const queryParams: QueryParamsType = {
        pageNumber: currentPage,
        pageSize: pageSize,
        messageType: messageType,
      };

      if (searchQuery.trim() !== "") {
        queryParams.searchQuery = searchQuery;
      }

      const response = await authenticatedRequest(
        "https://localhost:7202/telegramMessages",
        {
          queryParams,
        }
      );
      setTelegramMessages(response.data.telegramMessages);
      setCountPages(response.data.totalPages);
      setIsLoadingMessages(false);
    } catch (error) {
      setIsLoadingError(true);
      console.error("Ошибка при получении данных:", error);
      setIsLoadingMessages(false);
      setErrorMessage(toErrorMessage(error));
    }
  };

  useEffect(() => {
    setIsLoadingMessages(true);
    fetchData();
    window.scrollTo(0, 0);
  }, [messageType, currentPage, pageSize]);

  useEffect(() => {
    setCurrentPage(1);

    if (searchTimeout) {
      clearTimeout(searchTimeout);
    }

    const newSearchTimeout = setTimeout(() => {
      fetchData();
    }, 700);

    setSearchTimeout(newSearchTimeout);
  }, [searchQuery]);

  const handleMessageTypeChange = (type: any) => {
    setMessageType(type);
    setCurrentPage(1);
  };

  const handlePageSize = (size: number) => {
    setPageSize(size);
    setCurrentPage(1);
  };

  return (
    <>
      {isLoadingError ? (
        <h2 className="text-red-600 text-2xl text-center mt-16">
          {errorMessage}
        </h2>
      ) : isLoadingMessages ? (
        <span className="block mt-10">
          <LoadingSpinner />
        </span>
      ) : (
        <>
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
          <CRMMessageTable
            telegramMessages={telegramMessages}
            lyteVersion={false}
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
              <option value="10">10</option>
              <option value="25">25</option>
              <option value="50">50</option>
              <option value="100">100</option>
            </select>
          </div>
        </>
      )}
    </>
  );
};

export default CRMMessagePage;
