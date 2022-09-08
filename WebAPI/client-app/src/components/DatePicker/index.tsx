import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { TextFieldFirstStyle } from '../TextField/styled';
export interface IDatePicker {
    label: string,
    touched?: boolean | undefined,
    error?: string | undefined,
    value: Date,
    onChange: (e: any, value: any) => void,
};

const DatePickerComponent: React.FC<IDatePicker> = ({ value, onChange, label, touched, error }) => {
    return (
        <LocalizationProvider dateAdapter={AdapterDateFns}>
            <DatePicker
                value={value}
                label={label}
                onChange={onChange}
                inputFormat="dd/MM/yyyy"
                renderInput={(props) => (
                    <TextFieldFirstStyle
                        helperText={touched && error}
                        variant="standard"
                        fullWidth
                        {...props}
                    />
                )}
            />
        </LocalizationProvider>
    );
}

export default DatePickerComponent;