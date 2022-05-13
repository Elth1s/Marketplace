import { combineReducers } from "redux";

import { authReducer } from "../../components/auth/reducer";
import { profileReducer } from "../../components/user/Profile/reducer";
import { categoryReducer } from "../../components/category/reducer";

export const rootReducer = combineReducers({
    auth: authReducer,
    profile: profileReducer,
    category: categoryReducer,
});

export type RootState = ReturnType<typeof rootReducer>;