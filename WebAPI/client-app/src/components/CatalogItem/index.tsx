import { Paper, Typography, useTheme } from '@mui/material'
import { FC } from 'react'

import LinkRouter from '../LinkRouter'
import { BoxStyle, ImageBoxStyle } from './styled'

import { small_empty } from '../../assets/backgrounds'

interface Props {
    name: string,
    image: string,
    urlSlug: string
}

const CatalogItem: FC<Props> = ({ name, image, urlSlug }) => {
    const { palette } = useTheme();

    return (
        <LinkRouter underline="none" color="unset" to={`/catalog/${urlSlug}`} sx={{ marginRight: "15px", marginBottom: "15px", }}>
            <BoxStyle>
                <ImageBoxStyle>
                    <img
                        style={{ width: "220px", height: "220px", objectFit: "contain" }}
                        src={image != "" ? image : small_empty}
                        alt="categoryImage"
                    />
                </ImageBoxStyle>
                <Paper elevation={0} sx={{ backgroundColor: palette.mode == "dark" ? "#2D2D2D !important" : "transparent", minHeight: "69px", maxHeight: "69px", overflow: "hidden" }}>
                    <Typography variant="h5" color="inherit" fontWeight="medium" align="center">
                        {name}
                    </Typography>
                </Paper>
            </BoxStyle>
        </LinkRouter>
    )
}

export default CatalogItem