import { useEffect, useState } from "react";
import styles from "./DealsPage.module.scss";
import Pagination from "../../components/pagination/Pagination";
import CRMDealsTable from "../../components/crmDealsTable/CRMDealsTable";
import { QueryParamsType } from "../../types/QueryParamsType";
import { authenticatedRequest } from "../../utils/Request";
import { toErrorMessage } from "../../utils/ErrorHandler";
import { useDispatch, useSelector } from "react-redux";
import { setDeals } from "../../redux/slices/deal";
import LoadingSpinner from "../../components/loadingSpinner/LoadingSpinner";
import { RootState } from "../../redux/store";
const DealsPage = () => {
  const dealStatuses = [
    "All",
    "In Progress",
    "Pending",
    "Lost Deal",
    "Won Deal",
    "Cart",
    "Scam",
  ];
  const dispatch = useDispatch();
  const deals = useSelector((state: RootState) => state.dealSlice.Deals);
  const [isLoading, setIsLoading] = useState<Boolean>(false);
  const [isLoadingError, setIsLoadingError] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [activeDealStatus, setActiveDealStatus] = useState(0);
  const [countPages, setCountPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(25);
  const [searchQuery, setSearchQuery] = useState<string>("");
  const [searchTimeout, setSearchTimeout] = useState<NodeJS.Timeout | null>(
    null
  );
  const fetchData = async () => {
    try {
      const queryParams: QueryParamsType = {
        pageNumber: currentPage,
        pageSize: pageSize,
        dealStatus: activeDealStatus,
      };

      if (searchQuery.trim() !== "") {
        queryParams.searchQuery = searchQuery;
      }

      const response = await authenticatedRequest(
        "https://localhost:7151/deals/GetDealsByLoggedUser",
        {
          queryParams,
        }
      );
      dispatch(setDeals(response.data.deals));
      setCountPages(response.data.totalPages);
      setIsLoading(false);
    } catch (error) {
      setErrorMessage(toErrorMessage(error));
      setIsLoadingError(true);
      console.error("Ошибка при получении данных:", error);
      setIsLoading(false);
    }
  };

  const handlePageSize = (size: number) => {
    setPageSize(size);
    setCurrentPage(1);
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

  useEffect(() => {
    setIsLoading(true);
    window.scrollTo(0, 0);
    fetchData();
  }, [pageSize, currentPage, activeDealStatus]);

  return (
    <div className="">
      <h1 className="text-center text-3xl font-extrabold pt-6">Deals</h1>
      <ul className="mt-4 flex items-center px-10 justify-center mb-5">
        {dealStatuses.map((dealStatus, index) => (
          <li
            onClick={() => setActiveDealStatus(index)}
            className={
              activeDealStatus === index
                ? `${styles.dealStatusBox} ${styles.activeDealStatus}`
                : styles.dealStatusBox
            }
          >
            {dealStatus}
          </li>
        ))}
      </ul>
      {isLoadingError ? (
        <h2 className="text-red-600 text-2xl text-center mt-16">
          {errorMessage}
        </h2>
      ) : isLoading ? (
        <LoadingSpinner />
      ) : (
        <>
          <input
            className="bg-gray-600 px-3 py-1 rounded-sm"
            type="text"
            placeholder="Search..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
          />
          <CRMDealsTable deals={deals} />
          <div className="flex justify-between items-center mt-10">
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
    </div>
  );
};

export default DealsPage;
