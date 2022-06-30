import { combineReducers } from "redux";

import { authReducer } from "../../pages/auth/reducer";
import { profileReducer } from "../../pages/user/reducer";
import { categoryReducer } from "../../pages/admin/category/reducer";
import { characteristicGroupReducer } from "../../pages/admin/characteristicGroup/reducer";
import { characteristicNameReducer } from "../../pages/admin/characteristicName/reducer";
import { countryReducer } from "../../pages/admin/country/reducer";
import { cityReducer } from "../../pages/admin/city/reducer";

export const rootReducer = combineReducers({
    auth: authReducer,
    profile: profileReducer,
    category: categoryReducer,
    characteristicGroup: characteristicGroupReducer,
    characteristicName: characteristicNameReducer,
    country: countryReducer,
    city: cityReducer,
});

export type RootState = ReturnType<typeof rootReducer>;