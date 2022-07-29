import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import { styled } from '@mui/material/styles';


export const ListItemStyled = styled(ListItem)(({ theme }) => ({
    "&.MuiListItem-root": {
        borderBottom: "1px solid #000000",
        padding: "0 0 5px 0",
        marginBottom: "13px",
        "&:last-child": {
            borderBottom: "none",
            marginBottom: "0",
        },
        "&:before": {
            content: '""',
            background: theme.palette.primary.main,
            width: "13px",
            height: "48px",
            borderRadius: "9px",
            marginRight: "18px",
        }
    },
}))

export const ListItemTextStyled = styled(ListItemText)(() => ({
    "& .MuiListItemText-primary": {
        color: "#000000",
        fontSize: "14px",
        fontWeight: "500",
        lineHeight: "18px",
    },
    "& .MuiListItemText-secondary": {
        color: "#000000",
        fontSize: "14px",
        fontWeight: "500",
        lineHeight: "18px",
        marginTop: "2px"
    }
}))