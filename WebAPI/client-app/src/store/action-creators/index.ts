import * as AuthActionCreators from "../../pages/auth/actions"
import * as ProfileActionCreators from "../../pages/user/actions"
import * as CategoryActionCreators from "../../pages/admin/category/actions"
import * as CharacteristicGroupActionCreators from "../../pages/admin/characteristicGroup/actions"
import * as CharacteristicNameActionCreators from "../../pages/admin/characteristicName/actions"
import * as CharacteristicValueActionCreators from "../../pages/admin/characteristicValue/actions"
import * as FilterGroupActionCreators from "../../pages/admin/filterGroup/actions"
import * as FilterNameActionCreators from "../../pages/admin/filterName/actions"
import * as FilterValueActionCreators from "../../pages/admin/filterValue/actions"
import * as CountryActionCreators from "../../pages/admin/country/actions"
import * as ProductStatusActionCreators from "../../pages/admin/productStatus/actions"
import * as AdminProductActionCreators from "../../pages/admin/product/actions"
import * as ShopActionCreators from "../../pages/admin/shop/actions"
import * as CityActionCreators from "../../pages/admin/city/actions"
import * as UnitActionCreators from "../../pages/admin/unit/actions"
import * as UserActionCreators from "../../pages/admin/user/actions"
import * as CatalogActionCreators from "../../pages/default/Catalog/actions"
import * as ProductActionCreators from "../../pages/default/product/actions"
import * as BasketActionCreators from "../../components/Basket/actions";

const actions = {
    ...AuthActionCreators,
    ...ProfileActionCreators,
    ...CategoryActionCreators,
    ...CharacteristicGroupActionCreators,
    ...CharacteristicNameActionCreators,
    ...CharacteristicValueActionCreators,
    ...FilterGroupActionCreators,
    ...FilterNameActionCreators,
    ...FilterValueActionCreators,
    ...ProductStatusActionCreators,
    ...AdminProductActionCreators,
    ...ShopActionCreators,
    ...CountryActionCreators,
    ...CityActionCreators,
    ...UnitActionCreators,
    ...CatalogActionCreators,
    ...ProductActionCreators,
    ...BasketActionCreators,
    ...UserActionCreators,
}
export default actions;