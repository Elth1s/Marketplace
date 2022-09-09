import * as AuthActionCreators from "../../pages/auth/actions"
import * as ForgotPasswordActionCreators from "../../pages/auth/ForgotPassword/actions"

import * as ProfileActionCreators from "../../pages/user/actions"
import * as CategoryActionCreators from "../../pages/admin/category/actions"
import * as CharacteristicGroupActionCreators from "../../pages/seller/characteristicGroup/actions"
import * as CharacteristicNameActionCreators from "../../pages/seller/characteristicName/actions"
import * as CharacteristicValueActionCreators from "../../pages/seller/characteristicValue/actions"
import * as FilterGroupActionCreators from "../../pages/admin/filterGroup/actions"
import * as FilterNameActionCreators from "../../pages/admin/filterName/actions"
import * as FilterValueActionCreators from "../../pages/admin/filterValue/actions"
import * as CountryActionCreators from "../../pages/admin/country/actions"
import * as ProductStatusActionCreators from "../../pages/admin/productStatus/actions"
import * as SellerProductActionCreators from "../../pages/seller/product/actions"
import * as AdminShopActionCreators from "../../pages/admin/shop/actions"
import * as CityActionCreators from "../../pages/admin/city/actions"
import * as UnitActionCreators from "../../pages/admin/unit/actions"
import * as UserActionCreators from "../../pages/admin/user/actions"
import * as CatalogActionCreators from "../../pages/default/Catalog/actions"
import * as ProductActionCreators from "../../pages/default/product/actions"
import * as BasketActionCreators from "../../components/Basket/actions";
import * as ShopActionCreators from "../../pages/seller/CreateShopDialog/actions"
import * as OrderStatusActionCreators from "../../pages/admin/orderStatus/actions"
import * as ShopInfoActionCreators from "../../pages/default/ShortSellerInfo/actions"
import * as ShopPageActionCreators from "../../pages/default/SellerInfo/actions"
import * as DeliveryTypeActionCreators from "../../pages/admin/deliveryType/actions"
import * as SaleActionCreators from "../../pages/admin/sale/actions"
import * as UIActionCreators from "../../UISettings/actions"
import * as OrderCreators from "../../pages/seller/order/actions"

const actions = {
    ...AuthActionCreators,
    ...ForgotPasswordActionCreators,
    ...ProfileActionCreators,
    ...CategoryActionCreators,
    ...CharacteristicGroupActionCreators,
    ...CharacteristicNameActionCreators,
    ...CharacteristicValueActionCreators,
    ...FilterGroupActionCreators,
    ...FilterNameActionCreators,
    ...FilterValueActionCreators,
    ...ProductStatusActionCreators,
    ...SellerProductActionCreators,
    ...AdminShopActionCreators,
    ...CountryActionCreators,
    ...CityActionCreators,
    ...UnitActionCreators,
    ...CatalogActionCreators,
    ...ProductActionCreators,
    ...BasketActionCreators,
    ...UserActionCreators,
    ...ShopActionCreators,
    ...OrderStatusActionCreators,
    ...ShopInfoActionCreators,
    ...ShopPageActionCreators,
    ...SaleActionCreators,
    ...DeliveryTypeActionCreators,
    ...UIActionCreators,
    ...OrderCreators
}
export default actions;