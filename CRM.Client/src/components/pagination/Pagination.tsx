import React from "react";
import ReactPaginate from "react-paginate";
import styles from "./Pagination.module.scss";

type Props = {
  onChangePage: (pageNumber: number) => void | undefined;
  countPages: number;
  currentPage: number;
};

const Pagination: React.FC<Props> = ({
  onChangePage,
  countPages,
  currentPage,
}) => {
  return (
    <ReactPaginate
      className={styles.root}
      breakLabel="..."
      nextLabel=">"
      forcePage={currentPage}
      onPageChange={(e) => onChangePage(e.selected + 1)}
      pageRangeDisplayed={3}
      marginPagesDisplayed={1}
      pageCount={countPages}
      previousLabel="<"
      renderOnZeroPageCount={null}
    />
  );
};

export default Pagination;
