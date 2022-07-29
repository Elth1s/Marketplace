import { Dispatch } from "react";

import http, { setLocalRefreshToken, setLocalAccessToken } from "../../../http_comon"

import { ServerError } from "../../../store/types";
import { AuthUser } from "../../auth/actions";
import { AuthAction, IAuthResponse } from "../../auth/types";
import { ICreateShop } from "./types";

export const RegisterShop = (data: ICreateShop) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {
            let response = await http.post<IAuthResponse>('api/Shop/Create', { ...data })
            const tokens = response.data;
            setLocalAccessToken(tokens.accessToken);
            setLocalRefreshToken(tokens.refreshToken);

            AuthUser(tokens.accessToken, dispatch);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}