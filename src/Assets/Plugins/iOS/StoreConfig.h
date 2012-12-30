/*
 * Copyright (C) 2012 Soomla Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

/**
 * This file holds the store's configurations.
 */

/*
 if the value of this variable is true, metadata (or more specifically your IStoreAssets) that was previously
 saved in the local DB will be deleted every time you start the application.
 
 another way to delete just the METADATA table is to upgrade the database version.
 */
extern BOOL DB_VOLATILE_METADATA;

/*
 * do you want to print out debug messages?
 */
extern BOOL STORE_DEBUG;

extern NSString* SOOM_SEC;
