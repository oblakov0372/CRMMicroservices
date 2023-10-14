import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import styles from "./LoginPage.module.scss";

import { useDispatch } from "react-redux";
import { login } from "../../redux/slices/auth";
import LoadingSpinner from "../../components/loadingSpinner/LoadingSpinner";
import { anonymRequest } from "../../utils/Request";
import { toErrorMessage } from "../../utils/ErrorHandler";

type loginType = {
  username: string;
  password: string;
};

const LoginPage = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [isSubmiting, setIsSubmiting] = useState<boolean>(false);
  const [isError, setIsError] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [showPassword, setShowPassword] = useState<boolean>(false);

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    const data: loginType = {
      username: username,
      password: password,
    };

    try {
      setIsSubmiting(true);
      const response = await anonymRequest(
        "https://localhost:7205/accounts/login",
        { method: "post" },
        data
      );
      dispatch(login(response.data));
      setIsSubmiting(false);
      setIsError(false);
      navigate("/");
    } catch (error: any) {
      setIsError(true);
      if (
        error.code === "ERR_NETWORK" ||
        (error.response && error.response.status === 401)
      ) {
        setErrorMessage(toErrorMessage(error));
      } else if (error.response.status === 400) {
        setErrorMessage("Invalid username or password");
      }
      setIsSubmiting(false);
    }
  };

  return (
    <div className={styles.container}>
      <div className={styles.menu}>
        <div>
          <h1>Registration for new user</h1>
          <p>If you don't have an account yet, please register.</p>
        </div>
        <button>
          <Link to={"/registration"}>Registration</Link>
        </button>
      </div>
      <div className={styles.login}>
        <h1 className={styles.title}>Log in</h1>
        <form onSubmit={handleSubmit} className={styles.form}>
          <div className={styles.inputWrapper}>
            <input
              id="usernmae"
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              className={styles.input}
              placeholder="Username"
              required
            />
          </div>
          <div className={styles.inputWrapper}>
            <input
              id="password"
              type={showPassword ? "text" : "password"}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className={styles.input}
              placeholder="Password"
              required
            />
          </div>
          {isError && (
            <h2 className="text-red-600 mb-3 font-extrabold">{errorMessage}</h2>
          )}
          <div className={styles.inputWrapper}>
            <input
              id="show-password"
              type="checkbox"
              checked={showPassword}
              onChange={() => setShowPassword((prev: boolean) => !prev)}
            />
            <label className="ml-2" htmlFor="show-password">
              Show Password
            </label>{" "}
          </div>
          <button type="submit" className={styles.button}>
            Log in
          </button>
        </form>
      </div>
      {isSubmiting && (
        <div className={styles.submitingWrapper}>
          <div className={styles.loadingSpinner}>
            <LoadingSpinner />
          </div>
        </div>
      )}
    </div>
  );
};

export default LoginPage;
