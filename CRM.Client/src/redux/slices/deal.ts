import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { DealType } from "../../types/DealType";

interface DealState {
  Deals: DealType[];
}

const initialState: DealState = {
  Deals: [],
};

const authSlice = createSlice({
  name: "deals",
  initialState,
  reducers: {
    setDeals(state, action: PayloadAction<DealType[]>) {
      state.Deals = action.payload;
    },
    addDeal(state, action: PayloadAction<DealType>) {
      state.Deals.push(action.payload);
    },
    removeDeal(state, action: PayloadAction<DealType>) {
      state.Deals = state.Deals.filter((deal) => deal.id !== action.payload.id);
    },
  },
});

export const { setDeals } = authSlice.actions;

export default authSlice.reducer;
