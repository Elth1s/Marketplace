import * as AuthActionCreators from "../../components/auth/actions"
import * as ProfileActionCreators from "../../components/user/Profile/actions"
import * as CategoryActionCreators from "../../components/category/actions"

const actions = {
    ...AuthActionCreators,
    ...ProfileActionCreators,
    ...CategoryActionCreators,
}
export default actions;