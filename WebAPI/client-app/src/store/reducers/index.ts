import { combineReducers } from "redux";

import { authReducer } from "../../pages/auth/reducer";
import { profileReducer } from "../../pages/user/reducer";
import { categoryReducer } from "../../pages/admin/category/reducer";
import { characteristicGroupReducer } from "../../pages/admin/characteristicGroup/reducer";
import { characteristicNameReducer } from "../../pages/admin/characteristicName/reducer";
import { countryReducer } from "../../pages/admin/country/reducer";
import { cityReducer } from "../../pages/admin/city/reducer";
import { unitReducer } from "../../pages/admin/unit/reducer";
import { catalogReducer } from "../../pages/default/Catalog/reducer";
import { productReducer } from "../../pages/default/product/reducer";
import { basketReducer } from "../../components/Basket/reducer";

export const rootReducer = combineReducers({
    auth: authReducer,
    profile: profileReducer,
    category: categoryReducer,
    characteristicGroup: characteristicGroupReducer,
    characteristicName: characteristicNameReducer,
    country: countryReducer,
    city: cityReducer,
    unit: unitReducer,
    catalog: catalogReducer,
    product: productReducer,
    basket: basketReducer,
});

export type RootState = ReturnType<typeof rootReducer>;