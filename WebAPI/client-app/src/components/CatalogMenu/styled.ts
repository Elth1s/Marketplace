import { styled, Typography } from "@mui/material";




export const CategoryTypographyStyle = styled(Typography, {
    shouldForwardProp: (prop) => prop !== 'selected'
})<{ selected?: boolean; }>(({ theme, selected }) => ({
    color: selected ? theme.palette.primary.main : theme.palette.common.black,
}),
);