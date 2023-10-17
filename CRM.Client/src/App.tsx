import "./App.css";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import Header from "./components/header/Header";
import { Provider } from "react-redux";
import { store } from "./redux/store";
import LoginPage from "./pages/loginPage/LoginPage";
import RegistrationPage from "./pages/registrationPage/RegistrationPage";
import CRMPage from "./pages/crmPage/CRMPage";
import CRMMessagePage from "./pages/crmMessagePage/CRMMessagePage";
import CRMAccountingPage from "./pages/crmAccountingPage/CRMAccountingPage";
import CRMTelegramUserPage from "./pages/crmTelegramUserPage/CrmTelegramUserPage";
import UserDealsPage from "./pages/userDealsPage/userDealsPage";

function App() {
  return (
    <Provider store={store}>
      <Router>
        <div className="App">
          <Header />
          <div className="wrapper">
            <Routes>
              <Route path="/login" element={<LoginPage />}></Route>
              <Route
                path="/registration"
                element={<RegistrationPage />}
              ></Route>
              <Route path="/crm" element={<CRMPage />}>
                <Route path="messages" element={<CRMMessagePage />} />
                <Route path="accounting" element={<CRMAccountingPage />} />
                <Route path="deals" element={<UserDealsPage />} />
                <Route
                  path="accounting/:telegramAccountId"
                  element={<CRMTelegramUserPage />}
                />
              </Route>
            </Routes>
          </div>
        </div>
      </Router>
    </Provider>
  );
}

export default App;
