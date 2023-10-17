import { useState } from "react";
import styles from "./UserDealsPage.module.scss";
import Pagination from "../../components/pagination/Pagination";
import CRMDealsTable from "../../components/crmDealsTable/CRMDealsTable";
const UserDealsPage = () => {
  const dealStatuses = [
    "All",
    "In Progress",
    "Pending",
    "Lost Deal",
    "Won Deal",
    "Cart",
    "Scam",
  ];
  const [activeDealStatus, setActiveDealStatus] = useState(0);
  const [countPages, setCountPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(25);
  const [searchQuery, setSearchQuery] = useState<string>("");

  const handlePageSize = (size: number) => {
    setPageSize(size);
    setCurrentPage(1);
  };
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
      <input
        className="bg-gray-600 px-3 py-1 rounded-sm"
        type="text"
        placeholder="Search..."
        value={searchQuery}
        onChange={(e) => setSearchQuery(e.target.value)}
      />
      <CRMDealsTable />
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
    </div>
  );
};

export default UserDealsPage;
