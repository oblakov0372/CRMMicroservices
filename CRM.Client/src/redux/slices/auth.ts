import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import jwt_decode from "jwt-decode";

type DecodedTokenType = {
  nameid: string;
  unique_name: string;
  role: UserRole;
  nbf: number;
  exp: number;
  iat: number;
  iss: string;
  aud: string;
};

enum UserRole {
  Admin = "admin",
  User = "user",
}

interface AuthState {
  isLoggedIn: boolean;
  role: UserRole | null;
}

const initialState: AuthState = {
  isLoggedIn: false,
  role: null,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login(state, action: PayloadAction<string>) {
      state.isLoggedIn = true;
      const token = action.payload;
      const decodedToken: DecodedTokenType = jwt_decode(token);
      state.role = decodedToken.role;
      console.log(state.role);
      localStorage.setItem("jwt", token);
    },
    logout(state) {
      state.isLoggedIn = false;
      state.role = null;
      localStorage.removeItem("jwt");
    },
  },
});

export const { login, logout } = authSlice.actions;

export default authSlice.reducer;
