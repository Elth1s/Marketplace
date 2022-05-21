import { combineReducers } from "redux";

import { authReducer } from "../../components/auth/reducer";
import { profileReducer } from "../../components/user/Profile/reducer";
import { categoryReducer } from "../../components/category/reducer";
import { characteristicGroupReducer } from "../../views/characteristicGroup/reducer";
import { characteristicReducer } from "../../views/characteristic/reducer";

export const rootReducer = combineReducers({
    auth: authReducer,
    profile: profileReducer,
    category: categoryReducer,
    characteristicGroup: characteristicGroupReducer,
    characteristic: characteristicReducer,
});

export type RootState = ReturnType<typeof rootReducer>;