import { Outlet } from "react-router-dom";
import styles from "./CRMPage.module.scss";
import CRMHeader from "../../components/crmHeader/CRMHeader";

const CRMPage = () => {
  return (
    <div className={styles.crmContainer}>
      <CRMHeader />
      <Outlet />
    </div>
  );
};

export default CRMPage;
