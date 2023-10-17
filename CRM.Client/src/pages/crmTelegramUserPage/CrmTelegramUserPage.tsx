import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { anonymRequest } from "../../utils/Request";
import styles from "./CrmTelegramUserPage.module.scss";
import { formatDate } from "../../utils/Utils";
import LoadingSpinner from "../../components/loadingSpinner/LoadingSpinner";
import Pagination from "../../components/pagination/Pagination";
import { TelegramMessageType } from "../../types/TelegramMessageType";
import { TelegramAccountBaseType } from "../../types/TelegramAccountBaseType";
import { QueryParamsType } from "../../types/QueryParamsType";
import CRMMessageTable from "../../components/crmMessageTable/CRMMessageTable";
import TelegramUserSections from "../../components/telegramUserSections/TelegramUserSections";

const CRMTelegramUserPage = () => {
  const { telegramAccountId } = useParams();
  const [telegramAccountData, setTelegramAccountData] =
    useState<TelegramAccountBaseType>();

  const [telegramMessages, setTelegramMessages] = useState<
    TelegramMessageType[]
  >([]);
  const [countPages, setCountPages] = useState<number>(0);
  const [pageSize, setPageSize] = useState<number>(5);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [isLoadingMessages, setIsLoadingMessages] = useState<Boolean>(true);
  const [isLoadingUserData, setIsLoadingUserData] = useState<Boolean>(true);
  const [messageType, setMessageType] = useState<string>("all");
  const [searchQuery, setSearchQuery] = useState<string>("");
  const [searchTimeout, setSearchTimeout] = useState<NodeJS.Timeout | null>(
    null
  );
  const [activeSection, setActiveSection] = useState(0);
  const fetchData = async () => {
    setIsLoadingMessages(true);
    try {
      const queryParams: QueryParamsType = {
        pageNumber: currentPage,
        pageSize: pageSize,
        messageType: messageType,
      };

      if (searchQuery.trim() !== "") {
        queryParams.searchQuery = searchQuery;
      }

      const response = await anonymRequest(
        `https://localhost:7202/telegramMessages/${telegramAccountId}`,
        {
          queryParams,
        }
      );

      setTelegramMessages(response.data.telegramMessages);
      setCountPages(response.data.totalPages);
      setIsLoadingMessages(false);
    } catch (error) {
      console.error("Ошибка при получении данных:", error);
      setIsLoadingMessages(false);
    }
  };

  useEffect(() => {
    setIsLoadingMessages(true);
    fetchData();
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

  useEffect(() => {
    const fetchData = async () => {
      try {
        setIsLoadingUserData(true);
        const response = await anonymRequest(
          `https://localhost:7014/telegramUsers/GetTelegramUserData/${telegramAccountId}`
        );
        setTelegramAccountData(response.data);
        console.log(response.data);
        setIsLoadingUserData(false);
      } catch (error) {
        setIsLoadingUserData(false);
      }
    };
    fetchData();
  }, []);
  return (
    <>
      <div className="bg-black flex px-10 py-5">
        <div className={styles.left_side}>
          {isLoadingUserData ? (
            <LoadingSpinner />
          ) : (
            <div className={styles.basic_user_information}>
              <h1>
                {telegramAccountData?.userName === null
                  ? ""
                  : telegramAccountData?.userName}
              </h1>
              {telegramAccountData?.linkToUserTelegram ? (
                <a
                  target="_blank"
                  className={styles.to_telegram}
                  href={telegramAccountData?.linkToUserTelegram}
                >
                  Go to Telegram
                </a>
              ) : (
                ""
              )}
              <div>
                <h1>Chats Activity:</h1>
                <div className={styles.boxes}>
                  <div className={styles.chatActivity}>
                    <span>{telegramAccountData?.countAllMessages}</span>
                    <span>Total</span>
                  </div>
                  <div className={`${styles.chatActivity} ${styles.WTS}`}>
                    <span>{telegramAccountData?.countMessagesWts}</span>
                    <span>WTS</span>
                  </div>
                  <div className={`${styles.chatActivity} ${styles.WTB}`}>
                    <span>{telegramAccountData?.countMessagesWtb}</span>
                    <span>WTB</span>
                  </div>
                </div>
              </div>
              <div className={styles.details}>
                <h1>Details</h1>
                <div className={styles.info}>
                  <span className={styles.title}>Telegram User Id: </span>
                  <span className={styles.data}>{telegramAccountData?.id}</span>
                </div>
                <div className={styles.info}>
                  <span className={styles.title}>Telegram userName: </span>
                  <span className={styles.data}>
                    {telegramAccountData?.userName
                      ? telegramAccountData?.userName
                      : "None"}
                  </span>
                </div>
                <div className={styles.info}>
                  <span className={styles.title}>First Activity: </span>
                  <span className={styles.data}>
                    {formatDate(
                      telegramAccountData?.firstActivity
                        ? telegramAccountData?.firstActivity
                        : ""
                    )}
                  </span>
                </div>
                <div className={styles.info}>
                  <span className={styles.title}>Last Activity: </span>
                  <span className={styles.data}>
                    {formatDate(
                      telegramAccountData?.lastActivity
                        ? telegramAccountData?.lastActivity
                        : ""
                    )}
                  </span>
                </div>
                {telegramAccountData?.linkToFirstMessage && (
                  <div className={styles.info}>
                    <span className={styles.title}>
                      <a
                        href={telegramAccountData?.linkToFirstMessage}
                        target="_blank"
                      >
                        Link To First Message
                      </a>
                    </span>
                  </div>
                )}
              </div>
            </div>
          )}
        </div>
        <div className="w-full">
          <TelegramUserSections
            setActiveSection={setActiveSection}
            activeSection={activeSection}
          />
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
        </div>
      </div>
    </>
  );
};

export default CRMTelegramUserPage;
