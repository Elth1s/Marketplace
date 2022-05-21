import { combineReducers } from "redux";

import { authReducer } from "../../pages/auth/reducer";
import { profileReducer } from "../../pages/user/Profile/reducer";
import { categoryReducer } from "../../pages/admin/category/reducer";
import { characteristicGroupReducer } from "../../pages/admin/characteristicGroup/reducer";
import { characteristicReducer } from "../../pages/admin/characteristic/reducer";

export const rootReducer = combineReducers({
    auth: authReducer,
    profile: profileReducer,
    category: categoryReducer,
    characteristicGroup: characteristicGroupReducer,
    characteristic: characteristicReducer,
});

export type RootState = ReturnType<typeof rootReducer>;