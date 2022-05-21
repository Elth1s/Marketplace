import * as Yup from 'yup';

export const LogInSchema = Yup.object().shape({
    email: Yup.string().email().required().label('Email'),
    password: Yup.string().required().label('Password')
});

const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/;
const passwordRegExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{8,}$/

export const SignUpSchema = Yup.object().shape({
    firstName: Yup.string().min(3).max(50).required().label('First Name'),
    secondName: Yup.string().min(3).max(75).required().label('Second Name'),
    phone: Yup.string().matches(phoneRegExp, 'Phone number is not valid').required().label('Phone Number'),
    email: Yup.string().email().required().label('Email'),
    password: Yup.string().matches(passwordRegExp, 'Password is not valid').required().label('Password'),
    confirmPassword: Yup.string()
        .oneOf([Yup.ref('password'), null], 'Passwords must match').required().label('Confirm Password')
});