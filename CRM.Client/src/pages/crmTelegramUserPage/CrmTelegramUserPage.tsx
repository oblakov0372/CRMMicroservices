import { ChangeEvent, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { anonymRequest } from "../../utils/Request";
import styles from "./CrmTelegramUserPage.module.scss";
import { formatDate } from "../../utils/Utils";
import LoadingSpinner from "../../components/loadingSpinner/LoadingSpinner";
import { TelegramMessageType } from "../../types/TelegramMessageType";
import { TelegramAccountBaseType } from "../../types/TelegramAccountBaseType";
import { QueryParamsType } from "../../types/QueryParamsType";
import TelegramUserSections from "../../components/telegramUserSections/TelegramUserSections";
import { toErrorMessage } from "../../utils/ErrorHandler";
import MessageHistorySection from "../../components/messageHistorySection/MessageHistorySection";
import { Status } from "../../types/TelegramAccountLiteType";

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

  // Deal form state
  const [newDeal, setNewDeal] = useState({
    status: Status.None,
    telegramUserId: telegramAccountId,
  });
  const [isCreatingDeal, setIsCreatingDeal] = useState(false);

  // Handle input changes
  const handleDealChange = (
    e: ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setNewDeal({ ...newDeal, [name]: value });
  };

  // Handle deal creation
  const createDeal = async () => {
    setIsCreatingDeal(true);

    // Send a request to create the deal using 'newDeal' data
    console.log(newDeal);
    // After the request is complete, reset the form and update the UI
    setIsCreatingDeal(false);
    setNewDeal({ status: Status.None, telegramUserId: telegramAccountId });
  };

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
                  {telegramAccountData?.userName === null
                    ? ""
                    : telegramAccountData?.userName}
                </h1>
                {telegramAccountData?.linkToUserTelegram ? (
                  <a
                    target="_blank"
                    className={styles.to_telegram}
                    href={
                      telegramAccountData.linkToUserTelegram === "https://t.me/"
                        ? telegramAccountData.linkToFirstMessage
                        : telegramAccountData.linkToUserTelegram
                    }
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
                      {telegramAccountData?.telegramId}
                    </span>
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
            {activeSection === 1 && (
              <div>
                <h1 className="text-center text-2xl text-white mb-5">
                  Set user status
                </h1>
                <div className="p-4 border border-gray-700 rounded-md bg-gray-900">
                  <div className="mb-4">
                    <label
                      htmlFor="status"
                      className="block text-sm font-medium text-white mb-2"
                    >
                      Status
                    </label>
                    <select
                      id="status"
                      name="status"
                      value={newDeal.status}
                      onChange={handleDealChange}
                      className="mt-1 p-2 border border-gray-500 rounded-md w-full bg-gray-800 text-white"
                    >
                      <option value={Status.None}>None</option>
                      <option value={Status.Scamer}>Scamer</option>
                      <option value={Status.Reseller}>Reseller</option>
                      <option value={Status.Inwork}>In work</option>
                    </select>
                  </div>
                  <div className="mb-4"></div>
                  <button
                    onClick={createDeal}
                    className="px-4 py-2 text-white bg-blue-500 rounded-md hover:bg-blue-600"
                    disabled={isCreatingDeal}
                  >
                    {isCreatingDeal ? "Creating Deal..." : "Create Deal"}
                  </button>
                </div>
              </div>
            )}
          </div>
        </div>
      )}
    </>
  );
};

export default CRMTelegramUserPage;
