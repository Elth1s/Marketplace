import * as Yup from 'yup';


export const ProfileSchema = Yup.object().shape({
    firstName: Yup.string().min(2).max(15).required().label('First Name'),
    secondName: Yup.string().min(2).max(40).required().label('Second Name')
});

