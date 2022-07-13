import { Typography } from '@mui/material'
import { FC } from 'react'

import LinkRouter from '../LinkRouter'
import { BoxStyle, ImageBoxStyle } from './styled'

import { empty } from '../../assets/backgrounds'

interface Props {
    name: string,
    image: string,
    urlSlug: string
}

const CatalogItem: FC<Props> = ({ name, image, urlSlug }) => {
    return (
        <LinkRouter underline="none" color="unset" to={`/catalog/${urlSlug}`} sx={{ marginRight: "15px", marginBottom: "15px", }}>
            <BoxStyle>
                <ImageBoxStyle>
                    <img
                        style={{ width: "230px", height: "220px", objectFit: "contain" }}
                        src={image != "" ? image : empty}
                        alt="categoryImage"
                    />
                </ImageBoxStyle>
                <Typography variant="h5" fontWeight="medium" align="center">
                    {name}
                </Typography>
            </BoxStyle>
        </LinkRouter>
    )
}

export default CatalogItem