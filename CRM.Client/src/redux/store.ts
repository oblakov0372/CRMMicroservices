import { configureStore } from "@reduxjs/toolkit";
import authSlice from "./slices/auth";
import dealSlice from "./slices/deal";
export const store = configureStore({
  reducer: {
    authSlice,
    dealSlice,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
