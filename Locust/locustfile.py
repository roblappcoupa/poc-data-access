from locust import HttpUser, task, between
import random
import uuid
from datetime import datetime, timedelta

class UserBehavior(HttpUser):
    # Wait time between tasks to simulate real user behavior
    wait_time = between(1, 3)

    # Store created user IDs to ensure there are IDs available for get operations
    user_ids = []

    # Task to create users
    @task(1)
    def create_user(self):
        user_id = str(uuid.uuid4())
        payload = {
            "name": f"User_{user_id}",
            "details": f"Details about User_{user_id}",
            "birthday": (datetime.now() - timedelta(days=random.randint(18 * 365, 50 * 365))).strftime("%Y-%m-%d")
        }
        with self.client.post("/api/v1/person", json=payload, catch_response=True) as response:
            if response.status_code == 201:
                response.success()
                # Extract personId from response and add to the list for future get operations
                response_data = response.json()
                self.user_ids.append(response_data.get("personId"))
            else:
                response.failure(f"Failed to create user: {response.status_code}")

    # Task to get all users
    @task(2)
    def get_all_users(self):
        params = {
            "orderBy": "birthday"
        }
        with self.client.get("/api/v1/person", params=params, catch_response=True) as response:
            if response.status_code == 200:
                response.success()
            else:
                response.failure(f"Failed to get all users: {response.status_code}")

    # # Task to get a user by ID
    # @task(3)
    # def get_user_by_id(self):
    #     if not self.user_ids:
    #         # Skip the task if no users have been created yet
    #         return

    #     user_id = random.choice(self.user_ids)
    #     with self.client.get(f"/api/v1/person/{user_id}", catch_response=True) as response:
    #         if response.status_code == 200:
    #             response.success()
    #         elif response.status_code == 404:
    #             response.failure(f"User not found: {user_id}")
    #         else:
    #             response.failure(f"Failed to get user: {response.status_code}")

