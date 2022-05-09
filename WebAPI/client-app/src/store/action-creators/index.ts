import * as AuthActionCreators from "../../components/auth/actions"
import * as ProfileActionCreators from "../../components/user/Profile/actions"

const actions = {
    ...AuthActionCreators,
    ...ProfileActionCreators,
}
export default actions;