import { useEffect, useState } from "react";
import { QueryParamsType } from "../../types/QueryParamsType";
import { anonymRequest } from "../../utils/Request";
import LoadingSpinner from "../../components/loadingSpinner/LoadingSpinner";
import CrmAccountingTable from "../../components/crmAccountingTable/CRMAccountingTable";
import Pagination from "../../components/pagination/Pagination";
import { TelegramAccountLiteType } from "../../types/TelegramAccountType";
import { toErrorMessage } from "../../utils/ErrorHandler";

const CRMAccountingPage = () => {
  const [telegramsAccounts, setTelegramAccounts] = useState<
    TelegramAccountLiteType[]
  >([]);
  const [countPages, setCountPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(25);
  const [isLoading, setIsLoading] = useState<Boolean>(false);
  const [isLoadingError, setIsLoadingError] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [searchQuery, setSearchQuery] = useState<string>("");
  const [searchTimeout, setSearchTimeout] = useState<NodeJS.Timeout | null>(
    null
  );

  const fetchData = async () => {
    try {
      const queryParams: QueryParamsType = {
        pageNumber: currentPage,
        pageSize: pageSize,
      };

      if (searchQuery.trim() !== "") {
        queryParams.searchQuery = searchQuery;
      }

      const response = await anonymRequest(
        "https://localhost:7014/telegramUsers",
        {
          queryParams,
        }
      );
      setTelegramAccounts(response.data.telegramAccounts);
      setCountPages(response.data.totalPages);
      setIsLoading(false);
    } catch (error) {
      setErrorMessage(toErrorMessage(error));
      setIsLoadingError(true);
      console.error("Ошибка при получении данных:", error);
      setIsLoading(false);
    }
  };

  useEffect(() => {
    if (searchTimeout) {
      clearTimeout(searchTimeout);
    }

    const newSearchTimeout = setTimeout(() => {
      fetchData();
    }, 700);

    setSearchTimeout(newSearchTimeout);
  }, [searchQuery]);

  const handlePageSize = (size: number) => {
    setPageSize(size);
    setCurrentPage(1);
  };

  useEffect(() => {
    setIsLoading(true);
    window.scrollTo(0, 0);
    fetchData();
  }, [pageSize, currentPage]);
  return (
    <>
      {isLoadingError ? (
        <h2 className="text-red-600 text-2xl text-center mt-16">
          {errorMessage}
        </h2>
      ) : isLoading ? (
        <span className="block mt-10">
          <LoadingSpinner />
        </span>
      ) : (
        <>
          <div className="flex justify-between items-center bg-black px-7 py-2">
            <input
              className="bg-gray-600 px-3 py-1 rounded-sm"
              type="text"
              placeholder="Search..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
            />
          </div>
          <CrmAccountingTable telegramAccounting={telegramsAccounts} />
          <div className="flex justify-between items-center">
            {countPages > 1 && (
              <Pagination
                currentPage={currentPage - 1}
                countPages={countPages}
                onChangePage={setCurrentPage}
              />
            )}
            <select
              className="bg-gray-600  px-4 py-1 rounded-sm ml-7"
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

export default CRMAccountingPage;
