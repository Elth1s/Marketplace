import ListItemButton from '@mui/material/ListItemButton';
import { styled } from '@mui/material/styles';

export const ListItemButtonStyle = styled(ListItemButton)(({ theme }) => ({
    "&.MuiListItemButton-root": {
        padding: "0",
        "&:hover": {
            backgroundColor: "transparent",
        },
    },
    "&.Mui-selected": {
        backgroundColor: "transparent",
        "&:hover": {
            backgroundColor: "transparent",
        },
        "& .MuiTypography-root": {
            color: theme.palette.primary.main,
        },
    },
}));