System.config({
  baseURL: "/",
  defaultJSExtensions: true,
  transpiler: "typescript",
  paths: {
    "npm:*": "jspm_packages/npm/*",
    "github:*": "jspm_packages/github/*"
  },

  packages: {
    "js/main": {
      "main": "index.ts",
      "defaultExtension": "ts"
    }
  },

  map: {
    "jquery": "npm:jquery@2.2.0",
    "typescript": "npm:typescript@1.8.0"
  }
});
