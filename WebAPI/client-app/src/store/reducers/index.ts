import { combineReducers } from "redux";

import { authReducer } from "../../components/auth/reducer";
import { profileReducer } from "../../components/user/Profile/reducer";
import { categoryReducer } from "../../components/category/reducer";
import { characteristicGroupReducer } from "../../views/characteristicGroup/reducer";

export const rootReducer = combineReducers({
    auth: authReducer,
    profile: profileReducer,
    category: categoryReducer,
    characteristicGroup: characteristicGroupReducer,
});

export type RootState = ReturnType<typeof rootReducer>;