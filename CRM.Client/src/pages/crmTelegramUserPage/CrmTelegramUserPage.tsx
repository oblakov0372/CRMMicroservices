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
import { toErrorMessage } from "../../utils/ErrorHandler";
import MessageHistorySection from "../../components/messageHistorySection/MessageHistorySection";

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
  const [isLoadingMessages, setIsLoadingMessages] = useState<boolean>(true);
  const [isLoadingUserData, setIsLoadingUserData] = useState<boolean>(true);
  const [isLoadingError, setIsLoadingError] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [messageType, setMessageType] = useState<string>("all");
  const [searchQuery, setSearchQuery] = useState<string>("");
  const [searchTimeout, setSearchTimeout] = useState<NodeJS.Timeout | null>(
    null
  );
  //Deals,Message History
  const [activeSection, setActiveSection] = useState(0);
  const fetchData = async () => {
    setIsLoadingError(false);
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
      setIsLoadingError(true);
      setErrorMessage(toErrorMessage(error));
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
        setIsLoadingUserData(false);
      } catch (error) {
        setErrorMessage(toErrorMessage(error));
        setIsLoadingError(true);
        setIsLoadingUserData(false);
      }
    };
    fetchData();
  }, []);
  return (
    <>
      {isLoadingError ? (
        <h2 className="text-red-600 text-2xl text-center mt-16">
          {errorMessage}
        </h2>
      ) : (
        <div className="bg-black flex px-10 py-5">
          <div className={styles.left_side}>
            {isLoadingUserData ? (
              <LoadingSpinner />
            ) : (
              <div className={styles.basic_user_information}>
                <h1>
                  {telegramAccountData?.username === null
                    ? ""
                    : telegramAccountData?.username}
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
                    <span className={styles.data}>
                      {telegramAccountData?.id}
                    </span>
                  </div>
                  <div className={styles.info}>
                    <span className={styles.title}>Telegram userName: </span>
                    <span className={styles.data}>
                      {telegramAccountData?.username
                        ? telegramAccountData?.username
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
            {activeSection === 0 && (
              <MessageHistorySection
                searchQuery={searchQuery}
                setSearchQuery={setSearchQuery}
                messageType={messageType}
                handleMessageTypeChange={handleMessageTypeChange}
                isLoadingMessages={isLoadingMessages}
                telegramMessages={telegramMessages}
                countPages={countPages}
                currentPage={currentPage}
                setCurrentPage={setCurrentPage}
                pageSize={pageSize}
                handlePageSize={handlePageSize}
              />
            )}
          </div>
        </div>
      )}
    </>
  );
};

export default CRMTelegramUserPage;
