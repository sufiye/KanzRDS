import { create } from "zustand";
import { persist } from "zustand/middleware";

export const useTokens = create(
  persist(
    (set) => ({
      accessToken: "",
      refreshToken: "",
      roles: [], 
      loading: false,

      setLoading: (loadingState) =>
        set((state) => ({ ...state, loading: loadingState })),

      setAccessToken: (token) =>
        set((state) => ({ ...state, accessToken: token })),

      setRefreshToken: (token) =>
        set((state) => ({ ...state, refreshToken: token })),

      setRoles: (roles) =>
        set((state) => ({ ...state, roles: roles })), 

      clearTokens: () =>
        set(() => ({
          accessToken: "",
          refreshToken: "",
          roles: [], 
        })),
    }),
    { name: "tokens" }
  )
);