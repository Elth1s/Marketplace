import { combineReducers } from "redux";

import { authReducer } from "../../pages/auth/reducer";
import { profileReducer } from "../../pages/user/reducer";
import { categoryReducer } from "../../pages/admin/category/reducer";
import { characteristicGroupReducer } from "../../pages/admin/characteristicGroup/reducer";
import { characteristicNameReducer } from "../../pages/admin/characteristicName/reducer";
import { characteristicValueReducer } from "../../pages/admin/characteristicValue/reducer";
import { filterGroupReducer } from "../../pages/admin/filterGroup/reducer";
import { filterNameReducer } from "../../pages/admin/filterName/reducer";
import { filterValueReducer } from "../../pages/admin/filterValue/reducer";
import { productStatusReducer } from "../../pages/admin/productStatus/reducer";
import { productAdminReducer } from "../../pages/admin/product/reducer";
import { shopReducer } from "../../pages/admin/shop/reducer";
import { countryReducer } from "../../pages/admin/country/reducer";
import { cityReducer } from "../../pages/admin/city/reducer";
import { unitReducer } from "../../pages/admin/unit/reducer";
import { catalogReducer } from "../../pages/default/Catalog/reducer";
import { productReducer } from "../../pages/default/product/reducer";
import { basketReducer } from "../../components/Basket/reducer";
import { userReducer } from "../../pages/admin/user/reducer";

export const rootReducer = combineReducers({
    auth: authReducer,
    profile: profileReducer,
    category: categoryReducer,
    characteristicGroup: characteristicGroupReducer,
    characteristicName: characteristicNameReducer,
    characteristicValue: characteristicValueReducer,
    filterGroup: filterGroupReducer,
    filterName: filterNameReducer,
    filterValue: filterValueReducer,
    productStatus: productStatusReducer,
    productAdmin: productAdminReducer,
    shop: shopReducer,
    country: countryReducer,
    city: cityReducer,
    unit: unitReducer,
    catalog: catalogReducer,
    product: productReducer,
    basket: basketReducer,
    user: userReducer,
});

export type RootState = ReturnType<typeof rootReducer>;