export const authController = {
  controllerName: 'auth',
  actions: {
    login: 'Login',
    refreshTokenLogin: 'RefreshTokenLogin',
    googleLogin: 'google-login',
    facebookLogin: 'facebook-login',
  },
};

export const basketController = {
  controllerName: 'baskets',
  actions: {
    getBasketItems: '',
    addItemToBasket: '',
    updateQuantity: '',
    removeBasketItem: '',
  },
};

export const filesController = {
  controllerName: 'files',
  actions: {
    getBaseStorageUrl: 'GetBaseStorageUrl',
  },
};

export const productsController = {
  controllerName: 'products',
  actions: {
    get: '',
    getById: '',
    post: '',
    put: '',
    delete: '',
    upload: 'Upload',
    getProductImages: 'GetProductImages',
    deleteProductImage: 'DeleteProductImage',
    changeShowcase: 'ChangeShowcase',
  },
};

export const usersController = {
  controllerName: 'users',
  actions: {
    createUser: '',
  },
};
