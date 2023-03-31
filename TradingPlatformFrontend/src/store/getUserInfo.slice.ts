import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { API_SERVICE_URL, appURL } from "../config";
import { LoadingState } from "../model/loadingState";
import api from "./api";

export const getUserInfo = createAsyncThunk(
  "getUserInfo",
  async () => {
    let url = API_SERVICE_URL + appURL.getUserInfoURL;
    return await api.get(url);
  }
);

const initialState = {
  data: null,
  loading: LoadingState.Idle,
  error: null,
};

// Then, handle actions in your reducers:
const getUserInfoSlice = createSlice({
  name: "getUserInfo",
  initialState,
  reducers: {    
    getUserInfoEmpty(state, action) {
    // Use a "state machine" approach for loading state instead of booleans
    state.data = null;
    state.loading = LoadingState.Idle;
    state.error = null;
    }
  },
  extraReducers: {
    [getUserInfo.pending.type]: (state, _) => {
      if (state.loading === LoadingState.Idle) {
        state.loading = LoadingState.Pending;
      }
    },
    [getUserInfo.fulfilled.type]: (state, action) => {
      if (state.loading === LoadingState.Pending) {
        state.loading = LoadingState.Idle;
        state.data = action.payload.data;
      }
    },
    [getUserInfo.rejected.type]: (state, action) => {
      if (state.loading === LoadingState.Pending) {
        state.loading = LoadingState.Idle;
        state.error = action.error;
      }
    },
  },
});

export const getUserInfoReducer = getUserInfoSlice.reducer;
export const { getUserInfoEmpty } = getUserInfoSlice.actions