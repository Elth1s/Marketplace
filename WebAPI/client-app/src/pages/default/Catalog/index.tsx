import { Box, Typography } from '@mui/material';
import { useEffect } from 'react'

import BreadcrumbsComponent from '../../../components/BreadcrumbsComponent';
import CatalogItem from '../../../components/CatalogItem';

import { useActions } from '../../../hooks/useActions';
import { useTypedSelector } from '../../../hooks/useTypedSelector';
import { BoxCatalogStyle } from './styled';

const Catalog = () => {
    const { GetCatalog } = useActions();
    const { name, catalogItems } = useTypedSelector(state => state.catalog);

    useEffect(() => {
        document.title = name;
        getData();
    }, [])

    const getData = async () => {
        try {
            await GetCatalog();
        } catch (ex) {
        }
    };

    return (
        <Box>
            <BreadcrumbsComponent parents={[]} />
            <Typography variant='h1' sx={{ marginBottom: "30px" }}>
                Catalog
            </Typography>
            <BoxCatalogStyle>
                {catalogItems.map((row, index) => {
                    return (
                        <CatalogItem key={index} name={row.name} image={row.image} urlSlug={row.urlSlug} />
                    );
                })}
            </BoxCatalogStyle>
        </Box>
    );
}

export default Catalog