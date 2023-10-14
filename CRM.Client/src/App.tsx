import "./App.css";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import Header from "./components/header/Header";
import { Provider } from "react-redux";
import { store } from "./redux/store";
import LoginPage from "./pages/loginPage/LoginPage";
import RegistrationPage from "./pages/registrationPage/RegistrationPage";

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
            </Routes>
          </div>
        </div>
      </Router>
    </Provider>
  );
}

export default App;
