import { combineReducers } from "redux";

import { authReducer } from "../../components/auth/reducer";
import { profileReducer } from "../../components/user/Profile/reducer";

export const rootReducer = combineReducers({
    auth: authReducer,
    profile: profileReducer,
});

export type RootState = ReturnType<typeof rootReducer>;