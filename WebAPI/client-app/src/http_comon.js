import axios, { AxiosError } from "axios";
import { useTranslation } from "react-i18next";
import { accessToken as at, refreshToken as rt } from "./pages/auth/constants"

export const baseURL = `/${window.localStorage.getItem("i18nextLng")}/`


export function getLocalAccessToken() {
    const accessToken = window.localStorage.getItem(at);
    return accessToken;
}

function getLocalRefreshToken() {
    const refreshToken = window.localStorage.getItem(rt);
    return refreshToken;
}


export function setLocalAccessToken(token) {
    window.localStorage.setItem(at, token);
}

export function setLocalRefreshToken(token) {
    window.localStorage.setItem(rt, token);
}

export function toLowerFirstLetter(string) {
    return string.charAt(0).toLowerCase() + string.slice(1);
}

const instance = axios.create({
    baseURL: baseURL,
    headers: {
        "Content-Type": "application/json",
    },
});
instance.interceptors.request.use(
    (config) => {
        const token = getLocalAccessToken();
        if (token) {
            config.headers['Authorization'] = 'Bearer ' + token;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);
instance.interceptors.response.use(
    (res) => {
        return res;
    },
    async (err) => {
        const originalConfig = err.config;
        if (err.response) {
            // Validation failed, ...
            console.log("Interceptors", err.response)
            if (err.response.status === 400 && err.response.data) {
                return Promise.reject(err.response.data);
            }
            // Access Token was expired
            if (err.response.status === 401 && !originalConfig._retry && getLocalAccessToken() != null) {
                originalConfig._retry = true;
                try {
                    const rs = await refreshAccessToken();
                    const { accessToken, refreshToken } = rs.data;
                    setLocalRefreshToken(refreshToken)
                    setLocalAccessToken(accessToken)
                    instance.defaults.headers.common['Authorization'] = 'Bearer ' + accessToken;
                    return instance(originalConfig);
                } catch (_error) {
                    if (_error.response && _error.response.data) {
                        return Promise.reject(_error.response.data);
                    }
                    return Promise.reject(_error);
                }
            }
            if (err.response.status === 403 && err.response.data) {
                return Promise.reject(err.response.data);
            }
            // Backend not started, ...
            if (err.response.status === 404) {
                if (axios.isAxiosError(err)) {
                    return Promise.reject(err.response.data);
                }
                return;
                // Else Toast
            }
        }
        return Promise.reject(err);
    }
);

function refreshAccessToken() {
    return instance.post("api/Auth/RefreshToken", {
        token: getLocalRefreshToken(),
    });
}

export default instance;

