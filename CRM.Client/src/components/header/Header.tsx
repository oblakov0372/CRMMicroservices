import { RootState } from "../../redux/store";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import styles from "./Header.module.scss";
import logo from "../../assets/images/logo_symbol_transparent.png";
import { login, logout } from "../../redux/slices/auth";
import { useEffect } from "react";

const Header = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const isLoggedIn = useSelector(
    (state: RootState) => state.authSlice.isLoggedIn
  );

  const onClickLogout = () => {
    dispatch(logout());
    navigate("/login");
  };

  useEffect(() => {
    const token = localStorage.getItem("jwt");
    if (token) {
      dispatch(login(token));
    }
  }, []);

  return (
    <header className={styles.header}>
      <Link to={"/"}>
        <div className={styles.logo}>
          <img src={logo} alt="Crypto_0372" />
          <h2>Crypto_0372</h2>
        </div>
      </Link>
      <div className={styles.navigation}>
        <ul className={styles.navigationUl}>
          <li>
            <Link to={"/crm/messages"}>CRM</Link>
          </li>
        </ul>
      </div>
      <div className={styles.header__right}>
        <ul>
          {isLoggedIn ? (
            <li onClick={() => onClickLogout()}>
              <a>Logout</a>
            </li>
          ) : (
            <li>
              <Link to={"/login"}>Login</Link>
            </li>
          )}
        </ul>
      </div>
    </header>
  );
};

export default Header;
